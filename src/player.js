import Blob from './blob';

const { Body } = Phaser.Physics.Matter.Matter;
const { Query } = Phaser.Physics.Matter.Matter;

class Player extends Blob {
    constructor(scene) {
        super(scene);

        this.keys = scene.input.keyboard.createCursorKeys();
    }

    generateGeometry(radius = 20, color = 0x00FF00, lineColor = 0x000000) {
        super.generateGeometry(radius, color, lineColor);

        this.detector = this.scene.matter.add.circle(0, 0, 100, {
            label: 'PlayerDetector',
            isSensor: true,
        });

        this.target = this.scene.add.sprite(400, 600, 'playerTarget').setDisplaySize(90, 90)
        this.target.depth = 10000
        console.log(this.scene.textures.getTextureKeys())
    }

    update(_time, _dt) {
        let { x, y } = this.body.velocity;

        if (this.keys.left.isDown) x = -10;
        if (this.keys.right.isDown) x = 10;
        if (this.keys.down.isDown) y = 10;
        if (this.keys.up.isDown) y = -10;

        this.setVelocity({ x, y });

        Body.setPosition(this.detector, this.body.position)

        let collisions = Query.collides(this.detector, this.scene.blobs.map(blob => blob.body).filter(blob => blob !== this.body))
        if (collisions.length === 0) {
            this.target.setVisible(false)
        } else {
            let collision = collisions.sort((a, b) => { return b.depth - a.depth })[0]
            let detected = this.getDetectedBody(collision.bodyA, collision.bodyB)
            this.target.setPosition(detected.position.x, detected.position.y).setVisible(true)
        }
    }

    getDetectedBody(bodyA, bodyB) {
        let detected;
        if (bodyA === this.detector) {
            return bodyB;
        } else if (bodyB === this.detector) {
            return bodyA;
        } else {
            return
        }
    }
}

export default Player;
