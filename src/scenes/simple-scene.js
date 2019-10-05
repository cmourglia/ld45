import 'phaser';

export class Blob extends Phaser.GameObjects.GameObject {
	constructor(scene) {
		super();
		this.scene = scene
	}

	draw() {
		this.graphics = this.scene.add.graphics({
			x: this.x,
			y: this.y
		})
		this.graphics.beginFill(0xFF0000, 1);
		this.graphics.drawCircle(300, 300, 100);

		let matterEnabledContainer = this.scene.matter.add.gameObject(starGraphics);
		this.matterBody = this.scene.matter.add.circle(this.x, this.y, 40);
		matterEnabledContainer.setExistingBody(matterBody);
	}

	update() {
		this.matterBody.x = this.x
		this.matterBody.y = this.y
		this.graphics.x = this.x
		this.graphics.y = this.y
	}
};

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
		
		this.stars = []
        for (let i = 0; i < 10; ++i) {
            let starGraphics = this.add.graphics({
                                                    x: Math.random() * this.sys.game.canvas.width, 
                                                    y: Math.random() * this.sys.game.canvas.height, 
                                                }).setScale(0.2);
            drawStar(starGraphics, 0, 0,  5, 100, 50, 0xFFFF00, 0xFF0000);
            this.stars.push(starGraphics)

			let matterEnabledContainer = this.matter.add.gameObject(starGraphics);
			let matterBody = this.matter.add.circle(starGraphics.x, starGraphics.y, 40);
			matterEnabledContainer.setExistingBody(matterBody);
        }
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
	
function drawStar (graphics, cx, cy, spikes, outerRadius, innerRadius, color, lineColor) {
    var rot = Math.PI / 2 * 3;
    var x = cx;
    var y = cy;
    var step = Math.PI / spikes;
    graphics.lineStyle(10, lineColor, 1.0);
    graphics.fillStyle(color, 1.0);
    graphics.beginPath();
    graphics.moveTo(cx, cy - outerRadius);
    for (let i = 0; i < spikes; i++) {
        x = cx + Math.cos(rot) * outerRadius;
        y = cy + Math.sin(rot) * outerRadius;
        graphics.lineTo(x, y);
        rot += step;

        x = cx + Math.cos(rot) * innerRadius;
        y = cy + Math.sin(rot) * innerRadius;
        graphics.lineTo(x, y);
        rot += step;
    }
    graphics.lineTo(cx, cy - outerRadius);
    graphics.closePath();
    graphics.fillPath();
    graphics.strokePath();
}
