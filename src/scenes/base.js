import 'phaser';

export class Base extends Phaser.Scene {
    constructor() {
        super();
    }

    init(props) {
        this.level = props.level
    }

    createBounds() {
        let gameCanvas = this.sys.game.canvas
        let thickness = 10
        this.matter.world.setBounds(thickness, thickness, gameCanvas.width - 2 * thickness, gameCanvas.height - 2 * thickness, thickness * 10, true, true, true, true)
        this.add.rectangle(gameCanvas.width / 2, thickness / 2, gameCanvas.width, thickness, "000000")
        this.add.rectangle(gameCanvas.width / 2, gameCanvas.height - thickness / 2, gameCanvas.width, thickness, "000000")
        this.add.rectangle(thickness / 2, gameCanvas.height / 2, thickness, gameCanvas.height, "000000")
        this.add.rectangle(gameCanvas.width - thickness / 2, gameCanvas.height / 2, thickness, gameCanvas.height, "000000")

    }
}
