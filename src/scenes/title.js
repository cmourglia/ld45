import 'phaser';

import { Copulate } from './copulate';
import { Brawl } from './brawl';

export class Title extends Phaser.Scene {
    constructor() {
        super({
            key: 'Title'
        })
    }

    preload() {
        this.load.image("playButton", `assets/play.png`)
        this.scene.add("Copulate", Copulate, false, {});
        this.scene.add("Brawl", Brawl, false, {});
    }

    create() {
        let { width, height } = this.sys.game.canvas;
        this.add.image(width / 2, height / 2, "playButton").setDisplaySize(100, 100)

        // start the next level after 2.5 seconds
        this.time.addEvent({
            delay: 1,
            callback: () => {
                this.scene.start("Copulate", { level: 0 })
            }
        })
    }
}