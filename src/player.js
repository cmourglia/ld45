import { Blob } from './blob';

export class Player extends Blob {
    constructor(scene) {
        super(scene);

        this.keys = scene.input.keyboard.createCursorKeys();
    }

    generateGeometry(radius = 20, color = 0x00FF00, lineColor = 0x000000) {
        super.generateGeometry(radius, color, lineColor);
    }

    update(_time, _dt) {
        let { x, y } = this.body.velocity;

        if (this.keys.left.isDown) x = -10;
        if (this.keys.right.isDown) x = 10;
        if (this.keys.down.isDown) y = 10;
        if (this.keys.up.isDown) y = -10;

        if (x != 0 || y != 0) {
            this.setVelocity(x, y);
        }
    }
};
