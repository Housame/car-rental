<template>
    <div id="container">
        <h1>Return car</h1>
        <div class="box">
            <!-- Booking number -->
            <div class="field">
                <label class="label">Booking Number</label>
                <div class="control">
                    <input class="input" type="number" v-model="returnedCar.bookingNum" >
                </div>
            </div>

            <div class="field">
                <label class="label">Datum:</label>
                <div class="control">
                    <!-- <VueCtkDateTimePicker locale="sv">
                        
                    </VueCtkDateTimePicker> -->
                </div>
            </div> 

            <!-- Mileage -->
            <div class="field">
                <label class="label">Miltal:</label>
                <div class="control">
                    <input class="input" type="number" maxlength="6" v-model="returnedCar.incomingMileAge">
                </div>
            </div> 

            <div v-show="validSubmit" class="field">
                <label class="label">Att betala:</label>
                <div class="control">
                    <p>{{totPrice}}</p>
                </div>
            </div> 

            <div v-if="!validSubmit" class="field is-grouped">
                <div class="control">
                    <button class="button is-primary" @click="submit()" >Submit</button>
                </div>
                <div class="control">
                    <button class="button is-danger"  @click="choice()" >Avbryt</button>
                </div>
            </div>
            <div v-show="validSubmit" class="field is-grouped">
                <div class="control">
                    <button class="button is-primary"  @click="choice()" >Klart</button>
                </div>
            </div>
        </div>   
    </div>
    
</template>

<script>
import axios from "axios";
import moment from 'moment';

export default {
    data () {
        return {
            returnedCar:{
                bookingNum : Number,
                incomingDate : Date,
                incomingMileAge: Number
            },
            validSubmit : false,
            totPrice:0
        }
    },
    methods:{
        submit(){
            this.returnedCar.bookingNum = parseInt(this.returnedCar.bookingNum);
            var _date = moment().format('YYYY-MM-DD hh:mm:ss');
            this.returnedCar.incomingDate = _date;
            var returnCar = this.returnedCar;
            console.log("submit",returnCar);
            axios.post("https://localhost:7220/api/submitReturnedCar/",returnCar).then(response=>{
                console.log("totPrice", response.data);
                this.totPrice = response.data;
                this.validSubmit = true;

            });
        },
        choice(){
            this.$emit("choice");
        },
        mappingData(){
            this.returnedCar.bookingNum = parseInt(this.returnedCar.bookingNum);
            var _date = moment().format('YYYY-MM-DD hh:mm:ss');
            console.log(_date);
            this.returnedCar.incomingDate = _date;
            this.returnedCar.incomingMileAge = parseInt(this.returnedCar.incomingMileAge);
        },
        calculate(){
            this.mappingData();

            console.log(this.returnedCar );
            console.log(JSON.stringify(this.returnedCar) );
            var returnedCar = JSON.stringify(this.returnedCar);
            axios.get(`https://localhost:7220/api/getprice/`,returnedCar).then(response=>{
                console.log("totPrice", response.data);
                this.totPrice = response.data;
                this.validSubmit = true;
            });
        }
    }
}
</script>