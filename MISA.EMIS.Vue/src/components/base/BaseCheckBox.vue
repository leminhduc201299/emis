<template lang="">
    <div class="m-checkbox">
        <div 
          :class="{'icon-checkbox-active': isCheck, 'icon-checkbox-inactive': !isCheck}" 
          class="m-cursor-pointer checkbox-input"
          @click="onCheck"
          v-on:keyup.space="onCheck"
          :tabindex="tabindex"
        >
        </div>
        <div class="checkbox-text">{{ label }}</div>
    </div>
</template>

<script>
export default {
  name: "checkbox",

  data() {
    return {
      isCheck: false,
    };
  },

  methods: {
    /**
     * Method xử lý sự kiện khi nhấn vào ô checkbox
     */
    onCheck() {
      let currentCheck = this.isCheck;
      this.isCheck = !currentCheck;
      this.$emit("input", !currentCheck);
    },
  },

  props: {
    value: {
      type: Boolean,
      default: false,
    },
    label: {
      type: String,
      default: "",
    },
    tabindex: {
      type: Number,
      required: false,
      default: 0,
    },
  },

  mounted() {
    this.$emit("input", this.isCheck);
  },

  watch: {
    value: function (value) {
      if (typeof value === "boolean") {
        this.isCheck = value;
      }
    },
  },
};
</script>

<style scoped>
</style>