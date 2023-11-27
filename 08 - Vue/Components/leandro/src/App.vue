<template>
  <h1 class="title">Leandro</h1>
  <h2 class="subtitle">Single File Component</h2>

  <HelloWorld msg="Welcome to Your Vue.js App" />

  <h3 class="title is-size-4">01. Primer componente</h3>
  <div class="box">
    <Timer></Timer>
  </div>

  <h3 class="title is-size-4">02. Segundo componente</h3>
  <h4 class="subtitle is-size-5">Con inicializacion de propiedades</h4>
  <div class="box">
    <div><Icon type="fa-user" message="Hola, Leandro"></Icon></div>
    <div><Icon type="fa-user"></Icon></div>
  </div>

  <h3 class="title is-size-4">03. Tercer componente</h3>
  <h4 class="subtitle is-size-5">Ejemplo mas avanzado</h4>
  <div class="box">
    <Currency culture="en-US" :amount="amount"></Currency> |
    <Currency culture="es-AR" :amount="amount"></Currency> |
    <Currency culture="es-MX" :amount="amount"></Currency>
  </div>

  <h3 class="title is-size-4">04. Shopping Cart</h3>
  <h4 class="subtitle is-size-5">Componentes hijos y padres</h4>
  <div class="box">
    <ShoppingCart></ShoppingCart>
  </div>

  <h3 class="title is-size-4">05. Page View Component</h3>
  <h4 class="subtitle is-size-5">Uso de slots</h4>
  <div class="box">
    <PageContent title="Soy una pagina con SLOT">
      <p>Hola soy una pagina</p>
      <p>Hola mundo</p>
      <p>Leandro es la mejor plataforma</p>
      <p>
        Monto: <b>{{ amount }}</b>
      </p>
      <Icon type="fa-user" message="Hola, Leandro"></Icon>
    </PageContent>
  </div>

  <h3 class="title is-size-4">06. Third Party</h3>
  <h4 class="subtitle is-size-5">Paquetes de terceros</h4>
  <div class="box">
  </div>

</template>

<script>
import HelloWorld from "./components/HelloWorld.vue";
import Timer from "./components/Timer.vue";
import Icon from "./components/Icon.vue";
import Currency from "./components/Currency.vue";
import ShoppingCart from "./components/ShoppingCart.vue";
import PageContent from "./components/PageContent/PageContent.vue";

export default {
  name: "App",
  data() {
    return { amount: 100 };
  },
  components: {
    HelloWorld,
    Timer,
    Icon,
    Currency,
    ShoppingCart,
    PageContent,
  },
};
</script>

<style></style>
