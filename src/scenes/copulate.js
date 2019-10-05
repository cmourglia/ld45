import 'phaser';

export class Blob extends Phaser.GameObjects.GameObject {
	constructor(scene) {
		super(scene);
		this.scene = scene
	}

	draw(x = 0, y = 0,color = 0xFF0000, lineColor = 0x000000) {
		// Store params as members
		this.x = x
		this.y = y
		this.color = color
		this.lineColor = lineColor

		// Create the graphics object
		this.graphics = this.scene.add.graphics({
			x: this.x,
			y: this.y
		})
		this.graphics.lineStyle(5, lineColor, 1); 
		this.graphics.fillStyle(color, 1.0);
		this.graphics.beginPath();

		let radius = 60
		this.graphics.arc(0, 0, radius, 0, 2 * Math.PI);
		this.graphics.fillPath();
		this.graphics.strokePath();
 
		// Add a physic body to the graphics
		let matterEnabledContainer = this.scene.matter.add.gameObject(this.graphics);
		this.matterBody = this.scene.matter.add.circle(this.x, this.y, radius);
		matterEnabledContainer.setExistingBody(this.matterBody);
	}

	update() {
		// Bind object, graphics and physics body position
		this.matterBody.x = this.x
		this.matterBody.y = this.y
		this.graphics.x = this.x
		this.graphics.y = this.y
	}
};

export class Copulate extends Phaser.Scene {
	constructor() {
		super();
		this.text = null;
		this.x = 100;
		this.y = 100;

		this.speedX = 50;
		this.speedY = 50;
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
		
		// this.stars = []
        // for (let i = 0; i < 10; ++i) {
        //     let starGraphics = this.add.graphics({
        //                                             x: Math.random() * this.sys.game.canvas.width, 
        //                                             y: Math.random() * this.sys.game.canvas.height, 
        //                                         }).setScale(0.2);
        //     drawStar(starGraphics, 0, 0,  5, 100, 50, 0xFFFF00, 0xFF0000);
        //     this.stars.push(starGraphics)

		// 	let matterEnabledContainer = this.matter.add.gameObject(starGraphics);
		// 	let matterBody = this.matter.add.circle(starGraphics.x, starGraphics.y, 40);
		// 	matterEnabledContainer.setExistingBody(matterBody);
		// }
		
		for (let i = 0; i < 10; ++i) {
			let b = new Blob(this)
			b.draw()
		}
	}

	update(_time, dt) {
		if (this.enter.isDown) {
			this.scene.start("Brawl", { level: this.level })
		}

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

function drawStar(graphics, cx, cy, spikes, outerRadius, innerRadius, color, lineColor) {
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
