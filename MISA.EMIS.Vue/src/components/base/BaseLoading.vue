<template lang="">
    <div v-show="isLoading" class="m-loading">
      <div class="m-loading-modal"></div>
      <div class="m-loading-icon"></div>
    </div>
</template>

<script>
import EventBus from '../../utils/eventBus'

export default {
    name: "base-loading",

    data() {
        return {
            isLoading: false
        }
    },

    methods: {
        /**
         * Method xử lý hiển thị loading
         * Author: LMDuc (22/11/2021)
         */
        showLoadingHandle() {
            this.isLoading = true;
        },

        /**
         * Method xử lý ẩn loading
         * Author: LMDuc (22/11/2021)
         */
        hideLoadingHandle() {
            this.isLoading = false;
        }
    },

    created() {
        // Listening the event showToastMsg
        EventBus.$on("showLoading", this.showLoadingHandle);
        EventBus.$on("hideLoading", this.hideLoadingHandle);
    },
    destroyed() {
        // Stop listening the event showToastMsg with handler
        EventBus.$off("showLoading", this.showLoadingHandle);
        EventBus.$off("hideLoading", this.hideLoadingHandle);
    },
}
</script>

<style ccoped>
    
</style>