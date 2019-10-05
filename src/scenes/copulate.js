import Base from './base';

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
        this.enter = this.input.keyboard.addKey('ENTER');
    }

    update(time, dt) {
        super.update(time, dt);

        if (this.enter.isDown) {
            this.scene.start('Brawl', { level: this.level });
        }
    }
}

export default Copulate;
