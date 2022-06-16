<template>
  <div id="app" class="container">
    <img alt="Vue logo" src="./assets/car-rental-logo.jpg">
    <Reservation msg="Car rental app"/>
    

  </div>
</template>

<script>

import Reservation from './components/reservation/Reservation'

export default {
  name: 'App',
  components: {
    Reservation
  }
}
</script>

<style>
@import "~bulma/css/bulma.css";
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
img{
  height: 300px;
  width: 300px;
}
</style>
