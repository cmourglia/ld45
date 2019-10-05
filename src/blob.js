import Phaser from 'phaser';

const degToRad = Math.PI / 180.0;
const { Body } = Phaser.Physics.Matter.Matter;

class Shape {
    constructor(graphics, color) {
        this.graphics = graphics;
        this.color = color;
    }

    drawOn(_graphics) {
        throw new Error('Remi Croutel');
    }
}

class Arc extends Shape {
    constructor(graphics, color, radius) {
        super(graphics, color);
        this.radius = radius;
    }

    drawOn(graphics) {
        graphics.fillStyle(this.color, 1.0);
        graphics.fillRoundedRect(0, 0, this.radius, this.radius, { tl: 0, tr: 0, br: this.radius, bl: 0 })
    }
}

export class Quadrant {
    constructor(scene, shape) {
        this.scene = scene;
        this.shape = shape;
        this.subQuadrants = [];
    }

    addQuadrant(shape) {
        this.subQuadrants.push(new Quadrant(shape));
    }

    draw() {
        let g = this.scene.add.graphics();
        this.shape.drawOn(g);
        let texKey = Math.random().toString(36).substring(2, 15) + Math.random().toString(36).substring(2, 15);
        g.generateTexture(texKey, 60, 60)
        g.destroy()

        this.gameObject = this.scene.add.sprite(0, 0, texKey).setOrigin(0, 0)
    }

    traverse(callback) {
        callback(this);
        this.subQuadrants.forEach(callback);
    }
}

class Blob extends Phaser.GameObjects.GameObject {
    constructor(scene, specs) {
        super(scene);
        this.scene = scene;
        this.quadrants = [];
        this.specs = specs

        // By default: 4 blue circles with radius 30
        // const colors = [
        //     0xFF0000,
        //     0x00FF00,
        //     0xFF00FF,
        //     0xFFFF00,
        // ];
        for (let i = 0; i < 4; ++i) {
            this.quadrants.push(new Quadrant(this.scene, new Arc(this.scene, this.specs.color, this.specs.size)));
        }
    }

    generateGeometry(generateBody = true) {
        // Create the graphics object
        this.graphics = this.scene.add.graphics({ x: 0, y: 0 });

        // Draw the quadrants
        this.quadrants.forEach((q, i) => {
            q.draw();
            q.gameObject.setRotation((i * 90 - 135) * degToRad);
        });

        this.graphics = this.scene.add.container(0, 0, this.quadrants.map((x) => x.gameObject));
        this.graphics.parent = this

        // Add a physic body to the graphics
        if (generateBody) {
            this.matterEnabledContainer = this.scene.matter.add.gameObject(this.graphics);
            this.body = this.scene.matter.add.circle(0, 0, this.specs.size);
            this.matterEnabledContainer.setExistingBody(this.body);

            this.body.frictionAir = 0.05;
            this.body.friction = 0;
            this.body.frictionStatic = 0;
            this.body.restitution = 1;
        } else {
            this.matterEnabledContainer = this.graphics
        }
    }

    setVelocity(v) {
        Body.setVelocity(this.body, v);
    }

    setPosition(v) {
        if (this.body) {
            Body.setPosition(this.body, v);
        } else {
            this.matterEnabledContainer.setPosition(v.x, v.y)
        }
    }

    getPosition() {
        return this.body.position;
    }

    update(_time, _dt) {
        throw new Error('OVERRIDE ME DUMBASS');
    }

    destroy() {
        if (this.body) {
            this.scene.matter.world.remove(this.body);
        }
        this.matterEnabledContainer.destroy()
    }
}

export default Blob;
