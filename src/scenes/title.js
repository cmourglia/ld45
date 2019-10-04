import 'phaser';

import { SimpleScene } from './simple-scene';

export class Title extends Phaser.Scene {
    constructor() {
        super({
            key: 'Title'
        })
    }

    preload() {
        this.load.image("playButton", `assets/play.png`)
        this.scene.add("SimpleScene", SimpleScene, false, {});
    }

    create() {
        let { width, height } = this.sys.game.canvas;
        this.add.image(width / 2, height / 2, "playButton").setDisplaySize(100, 100)

        // start the next level after 2.5 seconds
        this.time.addEvent({
            delay: 2500,
            callback: () => {
                this.scene.start("SimpleScene")
            }
        })
    }
}