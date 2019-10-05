import Blob from './blob';

class Player extends Blob {
    constructor(scene) {
        super(scene);

        this.keys = scene.input.keyboard.createCursorKeys();
    }

    generateGeometry(radius = 20, color = 0x00FF00, lineColor = 0x000000) {
        super.generateGeometry(radius, color, lineColor);
    }

    update(_time, _dt) {
        let { x, y } = this.rootNodule.body.velocity;

        if (this.keys.left.isDown) x = -2;
        if (this.keys.right.isDown) x = 2;
        if (this.keys.down.isDown) y = 2;
        if (this.keys.up.isDown) y = -2;

        this.setVelocity(x, y);
    }
}

export default Player;
