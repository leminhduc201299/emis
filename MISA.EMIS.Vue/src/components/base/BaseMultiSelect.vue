<template>
  <div 
    :tabindex="tabindex" 
    @focusout="handleFocusout" 
    v-on:keyup.space="open = !open"
    v-on:keyup.down="onKeyDownUp"
    v-on:keyup.up="onKeyUpUp"
    v-on:keyup.enter="onItemClick(options[target].value)"
    class="m-combobox" 
    :class="{'m-combobox-full': fullWith}"  
    style="width: 208px;"
  >
      <!-- Show item selected -->
      <div v-for="(showOption, index) in showSelectedItem" :key="index"
        class="m-combobox-selected
      ">
        {{ showOption.text }}
        <span @click="onItemClick(showOption.value)" class="m-combbox-close icon-x"></span>
    </div>

    <span 
      v-show="selected ? (selected.length - showLength > 0) : false"
      class="m-badge-container"
    >
      <span class="m-dot">...</span>
      <span class="m-badge">{{ selected ? selected.length - showLength : '' }}</span>
    </span>

    <!-- Input -->
    <input type="text" class="m-combobox-input" :placeholder="placeholder">

    <!-- Button -->
    <button @click="open = !open" class="m-combobox-button icon-chevron"></button>

    <!-- Show list item -->
    <div v-show="open" class="m-combobox-select">
        <!-- Check all -->
        <div @click="onCheckAll" class="m-checkbox m-combobox-item m-combobox-item-all">
            <div 
                :class="{
                  'icon-checkbox-active': selected && selected.length && selected.length === options.length,
                  'icon-checkbox-inactive': !(selected && selected.length && selected.length === options.length)
                }" 
                class="m-cursor-pointer checkbox-input"></div>
            <div class="checkbox-text">Tất cả</div>
        </div>

        <div 
          v-for="(option, index) of options"
          :key="index"
          @click="onItemClick(option.value)"
          :class="{'m-combobox-target': option == (options ? options[target] : '')}"
          class="m-checkbox m-combobox-item"
        >
            <div 
                :class="{
                  'icon-checkbox-active': selected && selected.length > 0 ? selected.includes(option.value) : false,
                  'icon-checkbox-inactive': !(selected && selected.length > 0 ? selected.includes(option.value) : false)
                }" 
                class="m-cursor-pointer checkbox-input"
            ></div>
            <div class="checkbox-text">{{ option.text }}</div>
        </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "multi-select",

  props: {
    options: {
      type: Array,
      required: true,
    },
    value: {
      type: Array,
    },
    showLength: {
      type: Number,
      default: 1,
    },
    placeholder: {
      type: String,
      default: ''
    },
    fullWith: {
      type: Boolean,
      default: false
    },
    tabindex: {
      type: Number,
      required: false,
      default: 0,
    },
  },

  data() {
    return {
      selected: [],
      target: 0,
      open: false,
      checkAll: false
    };
  },

  methods: {
    /**
     * Method thực hiện chọn 1 item
     * Author: LMDuc (21/11/2021)
     */
    onItemClick(item) {
        if (this.selected.includes(item)) {
            this.selected = this.selected.filter(itemSelectd => itemSelectd !== item)
        } else {
            this.selected.push(item);
        }
      
        this.$emit('input', this.selected);
    },

    /**
     * Method thực hiện chọn tất cả item
     * Author: LMDuc (21/11/2021)
     */
    onCheckAll() {
        if (this.selected && this.selected.length && this.selected.length === this.options.length) {
            this.selected = []
        } else {
            this.selected = this.options.map(item => item.value);
        }
        
        this.$emit('input', this.selected);
    },

    /**
     * Method xử lý sự kiện khi focus out ra khỏi multi select
     * Author: LMDuc (21/11/2021)
     */
    handleFocusout(e) {
      // if the blur was because of outside focus
      // currentTarget is the parent element, relatedTarget is the clicked element
      if (!e.currentTarget.contains(e.relatedTarget)) {
          this.open = false;
      }
    },

    /**
     * Method xử lý sự kiện khi nhấn vào key down
     * Author: LMDuc (21/11/2021)
     */
    onKeyDownUp() {
      if (this.target == this.options.length - 1) {
        this.target = 0
      } else {
        this.target++;
      }
    },

    /**
     * Method xử lý sự kiện khi nhấn vào key up
     * Author: LMDuc (21/11/2021)
     */
    onKeyUpUp() {
      if (this.target == 0) {
        this.target = this.options.length - 1
      } else {
        this.target--;
      }
    }
  },

  mounted() {
    this.$emit("input", this.selected);
  },

  computed: {
    /**
     * Tính toán các item đã chọn để hiển thị
     * Author: LMDuc (21/11/2021)
     */
    showSelectedItem: function() {
        let showArray = this.selected.slice(0, this.showLength);
        let showOptions = this.options ? this.options.filter(item => showArray.includes(item.value)) : [];
        return showOptions; 
    }
  },

  watch: {
    value: function(value) {
        if (value && typeof(value) === 'object') {
            this.selected = value;
        } else {
          this.selected = [];
        }
    }
  }

};
</script>

<style scoped>
</style>