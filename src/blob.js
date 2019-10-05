import 'phaser'

export class Blob extends Phaser.GameObjects.GameObject {
	constructor(scene) {
		super(scene);
        this.scene = scene
	}

	draw(x = 0, y = 0, color = 0xFF0000, lineColor = 0x000000) {
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

	// update() {
	// 	// Bind object, graphics and physics body position
	// 	this.matterBody.x = this.x
	// 	this.matterBody.y = this.y
	// 	this.graphics.x = this.x
	// 	this.graphics.y = this.y
	// }
};
