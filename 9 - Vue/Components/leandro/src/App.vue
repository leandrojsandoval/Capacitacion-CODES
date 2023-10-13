<template>
  <h1 class="title">Leandro</h1>
  <h2 class="subtitle">Single File Component</h2>
  <HelloWorld msg="Welcome to Your Vue.js App"/>
</template>

<script>
import HelloWorld from './components/HelloWorld.vue'

export default {
  name: 'App',
  components: {
    HelloWorld
  }
}
</script>

<style></style>
