import Vue from 'vue'
import App from './App.vue'

// import VueCtkDateTimePicker from 'vue-ctk-date-time-picker';
// import 'vue-ctk-date-time-picker/dist/vue-ctk-date-time-picker.min.css';

// Vue.component(VueCtkDateTimePicker);
Vue.config.productionTip = false

new Vue({
  render: h => h(App),
}).$mount('#app')
