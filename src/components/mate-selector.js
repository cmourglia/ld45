import Phaser from 'phaser';

const { Body } = Phaser.Physics.Matter.Matter;
const { Query } = Phaser.Physics.Matter.Matter;

class MateSelector extends Phaser.GameObjects.GameObject {
    constructor(scene, player) {
        super(scene);

        this.player = player
        this.scene = scene

        this.detector = this.scene.matter.add.circle(0, 0, 100, {
            label: 'PlayerDetector',
            isSensor: true,
        });

        this.target = this.scene.add.sprite(400, 600, 'playerTarget').setDisplaySize(90, 90)
        this.target.depth = 0

        this.enter = this.scene.input.keyboard.addKey('ENTER');
    }

    preUpdate(time, delta) {
        Body.setPosition(this.detector, this.player.body.position)

        let detected
        let collisions = Query.collides(this.detector, this.scene.blobs.map(blob => blob.body).filter(blob => blob !== this.player.body))
        if (collisions.length === 0) {
            this.target.setVisible(false)
        } else {
            let collision = collisions.sort((a, b) => { return b.depth - a.depth })[0]
            detected = this.getDetectedBody(collision.bodyA, collision.bodyB)
            this.target.setPosition(detected.position.x, detected.position.y).setVisible(true)
        }

        if (this.enter.isDown) {
            if (detected) {
                this.scene.selectMate(detected.gameObject.parent)
            }
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

export default MateSelector;
