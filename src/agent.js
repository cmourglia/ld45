import Blob from './blob';

import Vector from './math/vector';

class Agent extends Blob {
    findNearest() {
        const { blobs } = this.scene;
        let closest = null;
        let minDistance = Number.MAX_VALUE;

        blobs.forEach((blob) => {
            if (blob !== this) {
                const d = Vector.distance(blob.getPosition(), this.getPosition());
                if (d < minDistance) {
                    minDistance = d;
                    closest = blob;
                }
            }
        });

        return closest;
    }

    update(_time, _dt) {
        const targetPosition = this.findNearest().getPosition();
        const currentPosition = this.getPosition();

        const distance = Vector.distance(targetPosition, currentPosition);
        if (distance > 10) {
            const dir = Vector.direction(currentPosition, targetPosition);
            this.setVelocity(Vector.mul(dir, 1));
        }
    }
}

export default Agent;
