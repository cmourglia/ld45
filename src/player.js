import Blob from './blob';

const { Body } = Phaser.Physics.Matter.Matter;
const { Query } = Phaser.Physics.Matter.Matter;

class Player extends Blob {
    constructor(scene, specs) {
        super(scene, specs);

        this.keys = scene.input.keyboard.createCursorKeys();
    }

    generateGeometry() {
        super.generateGeometry();
    }

    update(_time, _dt) {
        let { x, y } = this.body.velocity;

        if (this.keys.left.isDown) x = -10;
        if (this.keys.right.isDown) x = 10;
        if (this.keys.down.isDown) y = 10;
        if (this.keys.up.isDown) y = -10;

        this.setVelocity({ x, y });
    }
}

export default Player;
