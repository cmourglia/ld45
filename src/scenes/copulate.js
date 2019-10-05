import Base from './base';
import MateSelector from '../components/mate-selector';

class Copulate extends Base {
    init(props) {
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
        this.scene.start('Brawl', {
            level: this.level,
            mate: mate,
        });
    }
}

export default Copulate;
