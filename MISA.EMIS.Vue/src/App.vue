<template>
  <div>
    <router-view></router-view>
    <BaseToastMessage />
    <BasePopup />
    <BaseLoading />
  </div>
</template>

<script>
import BasePopup from "./components/base/BasePopup.vue";
import BaseToastMessage from "./components/base/BaseToastMessage.vue";
import BaseLoading from "./components/base/BaseLoading.vue";

export default {
  name: "App",

  components: {
    BasePopup,
    BaseToastMessage,
    BaseLoading,
  },
};
</script>

<style>
    @import url("./styles/main.css");
</style>