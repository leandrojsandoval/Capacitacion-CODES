<template>
  <span>{{ amountFormat }}</span>
</template>

<script>
export default {
  name: "CurrencyValue",
  props: {
    amount: {
      type: Number,
      required: true,
    },
    culture: String,
  },
  data() {
    return {
      currencies: [
        { culture: "es-AR", style: "ARG", value: 1000 },
        { culture: "en-US", style: "USD", value: 1 },
        { culture: "es-MX", style: "MXN", value: 19.08 },
      ],
    };
  },
  computed: {
    amountFormat() {
      let currency = this.currencies.find((x) => x.culture == this.culture);
      return (this.amount * currency.value).toLocaleString(currency.culture, {
        style: "currency",
        currency: currency.style,
      });
    },
  },
};
</script>