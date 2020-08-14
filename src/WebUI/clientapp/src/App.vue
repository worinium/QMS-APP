<template>
  <v-app>
      <navbar/>
      <v-main>
        <carousel/>
      </v-main>
      <footers/>
  </v-app>
</template>

<script>
import footers from './components/footers';
import carousel from './components/carousel';
import navbar from './components/navbar';

export default {
  name: 'App',

  components: {
    navbar, carousel, footers
  },

  data: () => ({
    //
  }),
};
</script>
