import 'phaser';

import { Blob } from '../blob';
 
export class Copulate extends Phaser.Scene {
	constructor() {
		super();
		this.text = null;
	}

	init(props) {
		this.level = props.level
	}

	preload() {
		this.load.image('poncho', 'assets/yolo.jpg');
	}

	create() {
        this.add.text(0, 0, "Copulate " + this.level, { fontFamily: 'Arial', fontSize: '100px' })
		this.enter = this.input.keyboard.addKey("ENTER")

        this.matter.world.setBounds(0, 0, 900, 900);
		
		for (let i = 0; i < 10; ++i) {
            let b = new Blob(this);
            b.generateGeometry();
		}
	}

	update(_time, dt) {
		if (this.enter.isDown) {
			this.scene.start("Brawl", { level: this.level })
		}
		}
}
