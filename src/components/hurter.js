import Phaser from 'phaser';

class Hurter extends Phaser.GameObjects.GameObject {
    constructor(scene, owner) {
        super(scene);

        this.owner = owner;
        this.scene = scene;

        this.enter = this.scene.input.keyboard.addKey('ENTER');

        const self = this;
        this.scene.matter.world.on('collisionstart', (event) => {
            event.pairs.forEach((pair) => {
                const detected = self.getDetectedBody(pair.bodyA, pair.bodyB);
                if (!detected) { return; }

                if (!detected.gameObject || !detected.gameObject.parent) { return; }
                detected.gameObject.parent.life -= pair.collision.depth;
            });
        });
    }

    preUpdate(time, delta) {
    }

    getDetectedBody(bodyA, bodyB) {
        if (bodyA === this.owner.body) {
            return bodyB;
        } if (bodyB === this.owner.body) {
            return bodyA;
        }
        return undefined;
    }
}

export default Hurter;
