<template>
    <div class="columns">
      <div class="column is-one-quarter box">
        <SideMenu/>
      </div>
      <div class="column box">
        <!-- <Choice v-if="step=1"/>
        <RentCar v-else-if="step=2"/>
        <ReturnCar v-else /> -->
        <component v-bind:is="selectedComponent" 
                  @rentCar="toRentCar" 
                  @returnCar="toReturnCar" 
                  @choice="toChoice"/>
      </div>
    </div>
</template>

<script>
import SideMenu from './SideMenu.vue'
import Choice from './Choice.vue'
import RentCar from './RentCar.vue'
import ReturnCar from './ReturnCar.vue'


export default {
  name: 'Reservation',
  components: {SideMenu,Choice,RentCar,ReturnCar},
  data(){
    return {
          selectedComponent : "Choice",
          bookedCars : [],
          setMockDataBtn: false
    }

  },
  methods:{
    toRentCar(){
      console.log("rent car btn");
      this.selectedComponent = "RentCar";
    },
    toReturnCar(){
      console.log("return car btn");
       this.selectedComponent = "ReturnCar";
    },
    toChoice(){
      console.log("Choice");
       this.selectedComponent = "Choice";
    },

  },
  props: {
    msg: String
  },
  created(){
    
  },
  // mounted(){
  //     this.intializeData();
  // }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
