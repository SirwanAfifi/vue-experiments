import Vue from 'vue';

import CoffeeComponent from './components/CoffeeComponent.vue';

Vue.config.productionTip = false;

export const eventBus = new Vue();

Vue.filter('number', function (value: any) {
    return value.toFixed(2);
})

const v = new Vue({
    el: '#app',
    data() {
        return {
            
        }
    },
    components: {
        'upload-file': CoffeeComponent
    }
});