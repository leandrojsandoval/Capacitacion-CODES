<template>
  <div>
    <div class="field">
      <button @click="reset" type="button">Reiniciar</button>
    </div>
    <ShoppingCartItems
      @getValues="setValues"
      ref="ShoppingCartItems"
    ></ShoppingCartItems>
    <div class="level has-text-centered">
      <div class="level-item">
        <div>
          <div class="heading">Total</div>
          <div class="title" style="color: black">${{ amount.toFixed(2) }}</div>
        </div>
      </div>
      <div class="level-item">
        <div>
          <div class="heading">Cantidad</div>
          <div class="title" style="color: black">{{ quantity }}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import ShoppingCartItems from "./ShoppingCartItems.vue";

export default {
  name: "ShoppingCart",
  components: {
    ShoppingCartItems,
  },
  data() {
    return {
      amount: 0,
      quantity: 0,
    };
  },
  methods: {
    setValues(obj) {
      this.amount = obj.amount;
      this.quantity = obj.quantity;
    },
    reset() {
      this.$refs.ShoppingCartItems.getAll();
    },
  },
};
</script>