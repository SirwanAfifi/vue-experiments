import axios from 'axios';
import { eventBus } from '../main';

class CoffeeService {

    saveCoffee(coffee: any, filesList: any): any {
        const formData = new FormData();
        for (const key in coffee) {
            if (coffee.hasOwnProperty(key)) {
                formData.append(key, coffee[key]);
            }
        }
        for (let i = 0; i < filesList.length; i++) {
            formData.append(filesList[i].name, filesList[i]);
        }

        let startTime = Date.now();

        return axios.post('/Coffees/SaveCoffee', formData, {
            onUploadProgress: uploadEvent => {
                const queueProgress = Math.round(uploadEvent.loaded / uploadEvent.total * 100);
                const timeElapsed = Date.now() - startTime;
                const uploadSpeedFirst = uploadEvent.loaded / (timeElapsed / 1000);
                const uploadTimeRemaining = Math.ceil(
                    (uploadEvent.total - uploadEvent.loaded) / uploadSpeedFirst
                  );
                const uploadTimeElapsed = Math.ceil(timeElapsed / 1000);
                const uploadSpeed = uploadSpeedFirst / 1024 / 1024;

                eventBus.$emit('uploadData', {
                    queueProgress,
                    uploadTimeRemaining,
                    uploadTimeElapsed,
                    uploadSpeed
                });
            }
        });
    }
}

// Export a singletone instance in the global namespace
export const coffeeService = new CoffeeService();