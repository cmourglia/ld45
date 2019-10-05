import Phaser from 'phaser';

class Base extends Phaser.Scene {
    init(props) {
        this.level = props.level;
    }

    createBounds() {
        const { width, height } = this.sys.game.canvas;
        const thickness = 10;

        this.matter.world.setBounds(thickness, thickness, width - 2 * thickness,
            height - 2 * thickness, thickness * 10);
        this.add.rectangle(width / 2, thickness / 2, width, thickness, 0x0);
        this.add.rectangle(width / 2, height - thickness / 2, width, thickness, 0x0);
        this.add.rectangle(thickness / 2, height / 2, thickness, height, 0x0);
        this.add.rectangle(width - thickness / 2, height / 2, thickness, height, 0x0);
    }
}

export default Base;
