/* eslint-disable no-underscore-dangle */
import Phaser from 'phaser';
import { throws } from 'assert';

const {
    Body, Bodies, Composite, Composites, Constraint,
} = Phaser.Physics.Matter.Matter;

class Nodule {
    constructor(scene, position, type, baseSize, color = 0xff00ff, generation = 0) {
        this.scene = scene;
        this.type = type;
        this.baseSize = baseSize;
        this.generation = generation;
        this.nodules = [];
        this.color = color;

        this.graphics = this.scene.add.graphics({ x: 0, y: 0 });
        this.radius = this.baseSize / (2 ** this.generation);

        // if (type === 'spike') {
        //     // add bodies
        //     const group = Body.nextGroup(true);
        //     const ropeA = Composites.stack(100, 50, 8, 1, 10, 10, (x, y) => Bodies.rectangle(x, y, 50, 20, { collisionFilter: { group } }));
        //     Composites.chain(ropeA, 0.5, 0, -0.5, 0, { stiffness: 0.8, length: 2, render: { type: 'line' } });

        //     const c = Constraint.create({
        //         bodyB: ropeA.bodies[0],
        //         pointB: { x: -25, y: 0 },
        //         pointA: { x: ropeA.bodies[0].position.x, y: ropeA.bodies[0].position.y },
        //         stiffness: 0.5,
        //     });
        //     Composite.add(ropeA, c);
        //     this.body = ropeA.bodies[0];
        //     this.scene.matter.world.add([this.body, c]);
        // } else {

        this.graphics.translateCanvas(50, 50);
        // draw core
        this.graphics.fillStyle(this.color);
        this.graphics.fillCircle(0, 0, this.radius);

        // Add a physic body to the graphics
        // const matterEnabledContainer = this.scene.matter.add.gameObject(graphics);
        this.body = Bodies.circle(position.x, position.y, this.radius);
        // this.body = Bodies.rectangle(position.x - this.radius, position.y - this.radius, this.radius * 2, this.radius * 2);
        // matterEnabledContainer.setExistingBody(this.body);
        // }

        const t = String(Math.random());
        this.graphics.generateTexture(t, 100, 100);
        this.graphics.destroy();
        this.graphics = this.scene.add.image(0, 0, t);
    }

    addNodule(type) {
        const offset = Math.PI / 4;
        const lastIndex = this.nodules.length - 1;
        const nnAngleRad = 2 * lastIndex * offset;
        const axis = {
            x: Math.cos(nnAngleRad),
            y: Math.sin(nnAngleRad),
        };

        const position = {
            x: 1.5 * this.radius * axis.x + this.body.position.x,
            y: 1.5 * this.radius * axis.y + this.body.position.y,
        };

        const nn = new Nodule(this.scene, position, type,
            this.baseSize, this.color, this.generation + 1);
        this.nodules.push(nn);
    }

    update() {
        const { x, y } = this.body.position;
        this.graphics.setPosition(x, y);
        this.nodules.forEach((nodule) => {
            nodule.update();
        });
    }
}

const getBodies = (nodule, outArray) => {
    outArray.push(nodule.body);
    nodule.nodules.forEach((n) => { getBodies(n, outArray); });
};

class Blob extends Phaser.GameObjects.GameObject {
    constructor(scene) {
        super(scene);
        this.scene = scene;
    }

    generateGeometry(radius = 50, color = 0xFF0000) {
        this.rootNodule = new Nodule(this.scene, { x: 0, y: 0 }, 'core', radius, color);

        for (let i = 0; i < 4; ++i) {
            this.rootNodule.addNodule('spike');
        }

        const bodies = [];
        getBodies(this.rootNodule, bodies);
        this.body = Body.create({ parts: bodies });
        this.scene.matter.world.add(this.body);
        this.setPosition({ x: 250, y: 250 });

        this.body.frictionAir = 0.01;
        this.body.friction = 0.1;
        this.body.frictionStatic = 0.1;
        this.body.restitution = 0.9;
    }

    setVelocity(v) {
        Body.setVelocity(this.body, v);
    }

    setPosition(v) {
        Body.setPosition(this.body, v);
    }

    getPosition() {
        return this.body.position;
    }

    update(_time, _dt) {
        this.rootNodule.update();
    }
}

export default Blob;
