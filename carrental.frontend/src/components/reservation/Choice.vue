<template>
    <div class="container">
        <div class="buttons is-centered">
            <button class="button is-primary "  @click="rentCar()">Hyr ut bil</button>
            <button class="button is-link"  @click="returnCar()">Ta emot bil</button>
        </div>
    </div>

</template>
<script>


export default {
    name: 'Choice',
    components:{},
    props:{},
    methods:{
    rentCar(){
      this.$emit("rentCar");
    },
    returnCar(){
      this.$emit("returnCar");
    }
  },

}
</script>
