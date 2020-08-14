<template>
  <v-card class="overflow-hidden">
    <v-app-bar
      absolute
      color="#43a047"
      dark
    >
      <!-- <template v-slot:img="{ props }">
        <v-img v-bind="props"
          gradient="to top right, rgba(55,236,186,.7), rgba(25,32,72,.7)"
        ></v-img>
      </template> -->

      <v-app-bar-nav-icon></v-app-bar-nav-icon>
      <v-toolbar-title>MediaHealth</v-toolbar-title>
      <v-toolbar-items class="hidden-sm-and-down">
              <v-btn flat>Home </v-btn>
               <v-btn flat>Services </v-btn>
              <v-btn flat>About</v-btn>
        </v-toolbar-items>
       <v-spacer></v-spacer>
        <v-toolbar-items class="hidden-sm-and-down">
              <v-btn flat>Sign Up </v-btn>
              <v-btn flat>login</v-btn>
        </v-toolbar-items>
    </v-app-bar>
    <v-sheet id="scrolling-techniques-5" class="overflow-y-auto" max-height="135">
      <v-container style="height: 600px;"></v-container>
    </v-sheet>
  </v-card>
</template>
