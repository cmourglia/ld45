import Base from './base';
import Blob from '../blob';
import MateSelector from '../components/mate-selector';

class Copulate extends Base {
    init(props) {
        super.init(props)

        this.level = props.level;
    }

    preload() {
        this.load.image('poncho', 'assets/yolo.jpg');
        this.load.image('playerTarget', 'assets/target.png')
        this.load.image('popup', 'assets/popup.png')
    }

    create() {
        super.create();

        this.add.text(0, 0, `Copulate ${this.level}`, { fontFamily: 'Arial', fontSize: '100px' });

        this.mateSelector = new MateSelector(this, this.player)
        this.add.updateList.add(this.mateSelector)
    }

    update(time, dt) {
        super.update(time, dt);


        if (this.enterUpAfterSelect && this.enterUpAfterSelect.isUp) {
            this.enterDownAfterSelect = this.enterUpAfterSelect
            this.enterUpAfterSelect = undefined
        }
        if (this.enterDownAfterSelect && this.enterDownAfterSelect.isDown) {
            this.enterDownAfterSelect = undefined
            this.changeScene('Brawl', {
                level: this.level,
            });
        }
    }

    selectMate(mate) {
        let oldPlayerSpecs = JSON.parse(JSON.stringify(this.player.specs))
        let playerSpecs = this.player.specs
        let mateSpecs = mate.specs
        this.player.specs.size = (playerSpecs.size + mateSpecs.size) / 2
        this.player.specs.color = (playerSpecs.color + mateSpecs.color) / 2

        this.mateSelector.destroy()

        let canvas = this.sys.canvas
        let center = { x: canvas.width / 2, y: canvas.height / 2 }
        this.add.image(center.x, center.y, "popup").setDisplaySize(canvas.width, canvas.height);

        const offsetParents = { x: 140, y: 75 }
        const offsetChildY = 145

        const b1 = new Blob(this, oldPlayerSpecs);
        b1.generateGeometry(false);
        b1.setPosition({ x: center.x - offsetParents.x, y: center.y - offsetParents.y });

        const b2 = new Blob(this, mateSpecs);
        b2.generateGeometry(false);
        b2.setPosition({ x: center.x + offsetParents.x, y: center.y - offsetParents.y });

        const b3 = new Blob(this, this.player.specs);
        b3.generateGeometry(false);
        b3.setPosition({ x: center.x, y: center.y + offsetChildY });

        this.enterUpAfterSelect = this.input.keyboard.addKey('ENTER');

        return
    }
}

export default Copulate;
