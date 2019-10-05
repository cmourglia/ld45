import Phaser from 'phaser';

import Player from '../player';
import Agent from '../agent';

class Base extends Phaser.Scene {
    constructor() {
        super();
        this.blobs = [];
    }

    init(props) {
        console.log(props)
        this.level = props.level;
        this.previousPlayerSpecs = props.previousPlayerSpecs
        this.previousBlobsSpecs = props.previousBlobsSpecs

        let self = this;
        this.events.on('shutdown', () => self.onShutdown())
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

        if (!this.previousPlayerSpecs) {
            this.previousPlayerSpecs = {
                color: 0x0000FF,
                size: 40,
            }
        }

        this.player = new Player(this, this.previousPlayerSpecs);
        this.player.generateGeometry();
        this.player.setPosition({ x: Phaser.Math.Between(100, 800), y: Phaser.Math.Between(100, 800) });
        this.blobs.push(this.player);


        if (!this.previousBlobsSpecs) {
            this.previousBlobsSpecs = []
        }

        // generate missing blobs
        for (let i = this.previousBlobsSpecs.length; i < 99; ++i) {
            this.previousBlobsSpecs.push({
                color: Phaser.Display.Color.HSVToRGB(Math.random(), .5 + Math.random() * .5, .5 + Math.random() * .5).color,
                size: 10 + Math.random() * 30,
            })
        }

        for (let i = 0; i < 99; ++i) {
            const b = new Agent(this, this.previousBlobsSpecs[i]);
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

    changeScene(sceneName, props) {
        props.previousPlayerSpecs = this.player.specs
        props.previousBlobsSpecs = this.blobs.map(blob => blob.specs)


        this.blobs.forEach(blob => {
            blob.destroy()
        })
        this.blobs = []

        this.scene.start(sceneName, props);
    }

    onShutdown() {
        this.events.off('shutdown')
        console.log("shutdown")
    }
}

export default Base;
