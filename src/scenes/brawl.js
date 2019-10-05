import 'phaser';

export class Brawl extends Phaser.Scene {
	constructor() {
		super();
		this.text = null;
	}

	init(props) {
		this.level = props.level
	}

	preload() {
	}

	create() {
		this.add.text(0, 0, "Brawl "+this.level, { fontFamily: 'Arial', fontSize: '100px' })
		this.enter = this.input.keyboard.addKey("ENTER")

	}

	update(_time, dt) {
		if (this.enter.isDown) {
			this.scene.start("Copulate", { level: this.level + 1 })
		}
	}
}
