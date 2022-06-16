<template>
    <div id="container">
        <h1>Rent Car</h1>
        <div class="box">
            <!-- Booking number -->
            <div class="field">
                <label class="label">Booking Number</label>
                <div class="control">
                    <input class="input" type="number" v-model="newBooking.bookingNum" :placeholder="newBooking.bookingNum" disabled>
                </div>
            </div>

            <!-- Social number -->
            <div class="field">
                <label class="label">Personnummer:</label>
                <div class="control">
                    <input class="input" type="number" v-model="newBooking.socialNumber" maxlength="12" placeholder="YYYYmmDDXXXX">
                </div>
            </div>

            <!-- Registration number -->
                <div class="field">
                <label class="label">Regnr:</label>
                <div class="control">
                    <div class="select">
                    <!-- <select v-model="newBooking.regNum">
                        <option disabled value="Tillgängliga bilar">Please select one</option> -->
                        <select v-model="newBooking.carId">
                            <option disabled value="Tillgängliga bilar">Please select one</option>
                            <option v-for="item in availableCars" v-bind:value="item.id" :key="item.id" >
                                {{item.regNum}}
                            </option>
                        <!-- <option>SJR509</option>
                        <option>XPR644</option> -->
                    </select>
                    </div>
                </div>
                </div>

                <div class="field">
                    <label class="label">Datum: </label><span>(ett bugg med date picker därför blir dert här automatiskt satt)</span>
                    <div class="control">
                        <p><b>{{newBooking.outgoingDate}}</b></p>
                        <!-- <VueCtkDateTimePicker locale="sv">
                            
                        </VueCtkDateTimePicker> -->
                    </div>
                </div> 

                <!-- Mileage -->
                <div class="field">
                    <label class="label">Miltal:</label>
                    <div class="control">
                        <input class="input" type="number" maxlength="6" placeholder="" v-model="newBooking.outgoingMileAge">
                    </div>
                </div> 

                <div class="field is-grouped">
                    <div class="control">
                        <button class="button is-primary" @click="submit()" >Submit</button>
                    </div>
                    <div class="control">
                        <button class="button is-danger"  @click="choice()" >Avbryt</button>
                    </div>
                </div>
        </div>   
    </div>
    
</template>

<script>
// import VueCtkDateTimePicker from 'vue-ctk-date-time-picker';
// import 'vue-ctk-date-time-picker/dist/vue-ctk-date-time-picker.css';
import Personnummer from 'personnummer';
import axios from "axios";
import moment from 'moment';

export default {
    name:'RentCar',
    components:{
        // VueCtkDateTimePicker
    },
    data () {
    return {
        newBooking:{
            bookingNum : Number,
            outgoingDate : Date,
            outgoingMileAge: Number,
            carId : Number,
            socialNumber: Number
        },
        availableCars:[]
      }
    },
    methods:{
        submit(){
            console.log("submit",this.newBooking);
            var reservation = this.newBooking;
            axios.post(`https://localhost:7220/api/newReservation`, reservation).then(response => {
            console.log("done",response.data);
            this.choice();
            }).catch(error =>{
                console.log("error",error)
            });

        },
        choice(){
            console.log("toChoice");
            this.$emit("choice");
        },
        validateSocNum(socNum){
            return Personnummer(socNum);
        },
        intializeData(){
            this.newBooking.outgoingDate = moment().format('YYYY-MM-DD hh:mm:ss')
            this.getBookingNum();
            this.getAvailaleCars();

        },
        getBookingNum(){
          axios.get("https://localhost:7220/api/newbookingNum").then(response=>{
          console.log("getBookingNum", response.data);
          this.newBooking.bookingNum = response.data;

          });
        },
        getAvailaleCars(){
          axios.get("https://localhost:7220/api/availablecars").then(response=>{
          console.log("availablecars", response.data);
          this.availableCars = response.data;
        //   this.availableCars = JSON.parse(JSON.stringify(response.data));
           });
        }
    },
    created(){
        this.intializeData();

    }
}
</script>
<style>
.bookingNum{
    font-weight: 200;
}
</style>