import Vue from 'vue';

import UploadFileComponent from './components/UploadFileSimpleComponent.vue';

Vue.config.productionTip = false;


const v = new Vue({
    el: '#app',
    data() {
        return {
            
        }
    },
    components: {
        'upload-file': UploadFileComponent
    }
});