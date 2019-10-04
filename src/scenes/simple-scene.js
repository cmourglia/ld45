import 'phaser';

export class SimpleScene extends Phaser.Scene {
	constructor() {
		super();
		this.text = null;
		this.x = 100;
		this.y = 100;

		this.speedX = 50;
		this.speedY = 50;
	}

	preload() {
		this.load.image('poncho', 'assets/yolo.jpg');
	}

	create() {
		this.image = this.add.image(this.x, this.y, 'poncho');
		this.lastTime = 0;
	}

	update(_time, dt) {
		this.x += this.speedX * (dt / 1000);
		this.y += this.speedY * (dt / 1000);

		const { width, height } = this.sys.game.canvas;
		const pxSize = 64;
		const hPxSize = pxSize / 2.0;

		if (this.x + hPxSize >= width || this.x - hPxSize <= 0) {
			this.speedX = -this.speedX;
		}

		if (this.y + hPxSize >= height || this.y - hPxSize <= 0) {
			this.speedY = -this.speedY;
		}

		this.image.setPosition(this.x, this.y);
	}
}
