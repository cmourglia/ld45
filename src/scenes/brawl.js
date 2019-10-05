import Base from './base';

class Brawl extends Base {
    constructor() {
        super();
        this.text = null;
    }

    init(props) {
        super.init(props)
        this.level = props.level;
        this.mate = props.mate;
    }

    create() {
        super.create();

        this.add.text(0, 0, `Brawl ${this.level}`, { fontFamily: 'Arial', fontSize: '100px' });
        this.enter = this.input.keyboard.addKey('ENTER');
    }

    update(time, dt) {
        if (this.enter.isDown) {
            this.changeScene('Copulate', {
                level: this.level + 1,
            });
        }

        super.update(time, dt);
    }
}

export default Brawl;
