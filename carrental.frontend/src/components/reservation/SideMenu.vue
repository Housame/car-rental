<template>
    <div>
        <div v-if="setMockDataBtn">
          <h1 >No results yet</h1>
          <button class="button is-primary ">Set mock data</button>
        </div>
        <div v-else-if="bookedCars.length > 0">
          <h1><b>Recent booked cars out</b></h1>
          <div class="content is-normal">
            <ul>
              <li v-for="item in bookedCars" :key="item.id">
                {{item.bookingNum}}
              </li>
            </ul>
          </div>
        </div>
        
    </div>
    
</template>
<script>
import axios from "axios";

export default {
  name: 'Sidemenu',
  components: {},
  props:{

  },
  data(){
    return {
          bookedCars : [],
          setMockDataBtn: false
    }

  },
  methods:{
         intializeData(){
         axios.get("https://localhost:7220/api/bookings").then(response=>{
          console.log("response",response.data.length, response.data);
          if(response.data.length === 0){this.setMockDataBtn = true} 
          // this.bookedCars = response.data;
          this.bookedCars = JSON.parse(JSON.stringify(response.data));
        });

    }
  },
  created(){
    this.intializeData();
    console.log(this.setMockDataBtn);
    console.log(this.bookedCars, " ---- ", this.bookedCars.length);
    console.log(JSON.parse(JSON.stringify(this.bookedCars)), " ---- ", this.bookedCars.length);
  }
}

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
