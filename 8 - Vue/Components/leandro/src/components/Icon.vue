<template>
  <span class="icon">
    <i class="fa" :class="type"></i>
  </span>
  <span v-if="message">{{ message }}</span>
</template>
  
<script>
export default {
  name: "IconDisplay",
  props: {
    type: {
      type: String,
      required: true,
    },
    message: {
      type: String,
    },
  },
};
</script>