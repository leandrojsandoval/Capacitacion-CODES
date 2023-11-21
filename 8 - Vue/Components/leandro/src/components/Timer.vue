<template>
  <div><span>Hora actual</span>: {{ time }}</div>
</template>

<script>
export default {
  name: "TimerDisplay",
  data() {
    return { time: "--" };
  },
  methods: {
    calculateTime() {
      setInterval(() => {
        let d = new Date();
        this.time = d.toLocaleTimeString();
      }, 1000);
    },
  },
  mounted() {
    this.calculateTime();
  },
};
</script>