<template>
    <div @click="navigate">
      <font-awesome-icon icon="bell" size="lg"></font-awesome-icon>
      <span class="badge badge-primary notification-badge">{{unreadNotificationsCount}}</span>
    </div>
</template>
<script>
import Vue from "vue";
import { library } from '@fortawesome/fontawesome-svg-core';
import { faBell } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { mapGetters } from "vuex";


library.add(faBell);

export default Vue.extend({
  name: "NotificationBell",
  components: {
    FontAwesomeIcon
  },
  computed: {
    ...mapGetters(['unreadNotificationsCount']),
  },
  methods: {
      navigate(){
          this.$router.push('/notifications');
      }
  }
});
</script>

<style lang="scss" scoped>
@import '@/scss/_variables';
div {
    cursor: pointer;
}

div:hover {
  color: $highlight-color;
}

.notification-badge {
  position: relative;
  right: 10px;
  top: -7px;
  font-size: 0.8em;
}
</style>