import Base from './base';
import MateSelector from '../components/mate-selector';

class Copulate extends Base {
    init(props) {
        super.init(props)

        this.level = props.level;
    }

    preload() {
        this.load.image('poncho', 'assets/yolo.jpg');
        this.load.image('playerTarget', 'assets/target.png')
    }

    create() {
        super.create();

        this.add.text(0, 0, `Copulate ${this.level}`, { fontFamily: 'Arial', fontSize: '100px' });

        this.add.updateList.add(new MateSelector(this, this.player))
    }

    update(time, dt) {
        super.update(time, dt);
    }

    selectMate(mate) {
        let playerSpecs = this.player.specs
        let mateSpecs = mate.specs
        this.player.specs.size = (playerSpecs.size + mateSpecs.size) / 2
        this.player.specs.color = (playerSpecs.color + mateSpecs.color) / 2

        // this.scene.start('Brawl', {
        //     level: this.level,
        //     mate: mate,
        //     previousPlayerSpecs: this.player.specs,
        //     previousBlobsSpecs: this.blobs.map(blob => blob.specs),
        // });

        this.changeScene('Brawl', {
            level: this.level,
            mate: mate,
        });
    }
}

export default Copulate;
