/* eslint-disable no-underscore-dangle */
import Phaser from 'phaser';

const { Body, Bodies, Constraint } = Phaser.Physics.Matter.Matter;

class Nodule {
    constructor(scene, position, type, baseSize, color = 0xff00ff, generation = 0) {
        this.scene = scene;
        this.type = type;
        this.baseSize = baseSize;
        this.generation = generation;
        this.nodules = [];
        this.color = color;

        const graphics = this.scene.add.graphics({ x: 0, y: 0 });
        this.radius = this.baseSize / 2 ** this.generation;

        // draw core
        graphics.fillStyle(this.color);
        graphics.fillCircle(0, 0, this.radius);

        // Add a physic body to the graphics
        const matterEnabledContainer = this.scene.matter.add.gameObject(graphics);
        this.body = Bodies.circle(position.x, position.y, this.radius);
        matterEnabledContainer.setExistingBody(this.body);

        // this.body.frictionAir = 0;
        this.body.friction = 0;
        this.body.frictionStatic = 0;
        this.body.restitution = 0;
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
            x: 2 * this.radius * axis.x + this.body.position.x,
            y: 2 * this.radius * axis.y + this.body.position.y,
        };

        const nn = new Nodule(this.scene, position, type, this.baseSize, this.color, this.generation + 1);
        this.nodules.push(nn);

        // Add constraints
        const { world } = this.scene.matter;

        const constraint = Constraint.create({
            bodyA: this.body,
            pointA: { x: axis.x * this.radius, y: axis.y * this.radius },
            bodyB: nn.body,
            pointB: { x: -axis.x * this.radius * 0.5, y: -axis.y * this.radius * 0.5 },
            stiffness: 0.01,
            damping: 0.1,
            // distance: 0,
        });

        world.add([this.body, nn.body, constraint]);
    }
}

class Blob extends Phaser.GameObjects.GameObject {
    constructor(scene) {
        super(scene);
        this.scene = scene;
    }

    generateGeometry(radius = 50, color = 0xFF0000) {
        this.rootNodule = new Nodule(this.scene, { x: 250, y: 250 }, 'core', radius, color);

        for (let i = 0; i < 4; ++i) {
            this.rootNodule.addNodule('spike');
        }
    }

    setVelocity(x, y) {
        Body.setVelocity(this.rootNodule.body, { x, y });
        // Body.applyForce(this.rootNodule.body, { x: 0, y: 0 }, { x, y });
    }

    setPosition(x, y) {
        Body.setPosition(this.rootNodule.body, { x, y });
    }

    getPosition() {
        return this.rootNodule.body.position;
    }

    update(_time, _dt) {
        throw new Error('OVERRIDE ME DUMBASS');
    }
}

export default Blob;
