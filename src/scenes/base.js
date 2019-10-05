import Phaser from 'phaser';

import Player from '../player';
import Agent from '../agent';

class Base extends Phaser.Scene {
    constructor() {
        super();
        this.blobs = [];
    }

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

    create() {
        this.createBounds();

        this.player = new Player(this);
        this.player.generateGeometry();
        this.player.setPosition({ x: Phaser.Math.Between(100, 800), y: Phaser.Math.Between(100, 800) });
        this.blobs.push(this.player);

        for (let i = 0; i < 99; ++i) {
            const b = new Agent(this);
            b.generateGeometry(10);
            b.setPosition({ x: Phaser.Math.Between(100, 800), y: Phaser.Math.Between(100, 800) });
            this.blobs.push(b);
        }
    }

    update(time, dt) {
        this.blobs.forEach((b) => {
            b.update(time, dt);
        });
    }
}

export default Base;
