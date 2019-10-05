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
        graphics.moveTo(0, 0);
        graphics.beginPath();
        graphics.lineStyle(0, this.color);
        graphics.fillStyle(this.color, 1.0);

        // First line
        graphics.lineBetween(0, 0, 0, -this.radius);

        // Second line
        graphics.lineBetween(0, 0, this.radius, 0);

        // Arc
        graphics.arc(0, 0, this.radius, 0, -0.5 * Math.PI, true);
        graphics.fillPath();
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
        this.gameObject = this.scene.add.graphics();
        this.shape.drawOn(this.gameObject);
        // graphics.rotateCanvas(45 * degToRad + (this.quadrantIndex + 1) * 90 * degToRad)
    }

    traverse(callback) {
        callback(this);
        this.subQuadrants.forEach(callback);
    }
}

class Blob extends Phaser.GameObjects.GameObject {
    constructor(scene) {
        super(scene);
        this.scene = scene;
        this.quadrants = [];

        // By default: 4 blue circles with radius 30
        const colors = [
            0xFF0000,
            0x00FF00,
            0xFF00FF,
            0xFFFF00,
        ];
        for (let i = 0; i < 4; ++i) {
            this.quadrants.push(new Quadrant(this.scene, new Arc(this.scene, colors[i], 30)));
        }
    }

    generateGeometry(radius = 20, color = 0xFF0000, lineColor = 0x000000) {
        // Store params as members
        this.color = color;
        this.lineColor = lineColor;

        // Create the graphics object
        this.graphics = this.scene.add.graphics({ x: 0, y: 0 });

        // Draw the quadrants
        this.quadrants.forEach((q, i) => {
            q.draw();
            q.gameObject.setRotation((i * 90 - 45) * degToRad);
        });

        this.graphics = this.scene.add.container(0, 0, this.quadrants.map((x) => x.gameObject));

        // this.graphics = this.scene.add.circle(0, 0, 10, 0xFF0000)

        // // Store params as members
        // this.x = x
        // this.y = y
        // this.color = color
        // this.lineColor = lineColor

        // // Create the graphics object
        // this.graphics = this.scene.add.graphics({
        //     x: this.x,
        //     y: this.y
        // })

        // this.graphics.lineStyle(2, lineColor, 1);

        // // Add a physic body to the graphics
        // this.graphics.fillStyle(color, 1.0);
        // this.graphics.beginPath();

        // this.graphics.arc(0, 0, radius, 0, 2 * Math.PI);
        // this.graphics.fillPath();
        // this.graphics.strokePath();

        // Add a physic body to the graphics
        const matterEnabledContainer = this.scene.matter.add.gameObject(this.graphics);
        this.body = this.scene.matter.add.circle(0, 0, radius);
        matterEnabledContainer.setExistingBody(this.body);

        this.body.frictionAir = 0.05;
        this.body.friction = 0;
        this.body.frictionStatic = 0;
        this.body.restitution = 1;
    }

    setVelocity(x, y) {
        Body.setVelocity(this.body, { x, y });
    }

    setPosition(x, y) {
        Body.setPosition(this.body, { x, y });
    }

    getPosition() {
        return this.body.position;
    }

    update(_time, _dt) {
        throw new Error('OVERRIDE ME DUMBASS');
    }
}

export default Blob;
