import Phaser from 'phaser';

import Base from './base';
import Player from '../player';
import Agent from '../agent';

class Copulate extends Base {
    constructor() {
        super();
        this.text = null;
        this.blobs = [];
    }

    init(props) {
        this.level = props.level;
    }

    preload() {
        this.load.image('poncho', 'assets/yolo.jpg');
    }

    create() {
        this.add.text(0, 0, `Copulate ${this.level}`, { fontFamily: 'Arial', fontSize: '100px' });
        this.enter = this.input.keyboard.addKey('ENTER');

        this.createBounds();

        const player = new Player(this);
        player.generateGeometry();
        player.setPosition(Phaser.Math.Between(100, 800), Phaser.Math.Between(100, 800));
        this.blobs.push(player);

        for (let i = 0; i < 100; ++i) {
            const b = new Agent(this, player);
            b.generateGeometry(10);
            b.setPosition(Phaser.Math.Between(100, 800), Phaser.Math.Between(100, 800));
            b.setVelocity(Phaser.Math.Between(-10, 10), Phaser.Math.Between(-10, 10));
            this.blobs.push(b);
        }
    }

    update(time, dt) {
        if (this.enter.isDown) {
            this.scene.start('Brawl', { level: this.level });
        }

        this.blobs.forEach((b) => {
            b.update(time, dt);
        });
    }
}

export default Copulate;
