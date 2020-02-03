<template>
  <b-container fluid>
    <b-row>
      <b-col cols="11">
        <b-row>
          <b-col>
            <hr />
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <b-nav-form>
              <b-form-input
                class="mr-2"
                v-model="SearchText"
                placeholder="Enter city name or zipcode"
              ></b-form-input>
              <b-button variant="info" v-on:click="getWeatherForecast()">Search</b-button>
            </b-nav-form>
          </b-col>
          <b-col>
            You selected:
            <b-badge variant="info">{{city}}</b-badge>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <hr />
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <b-card-group>
              <b-card
                v-for="item in dayItems"
                v-bind:key="item"
                bg-variant="info"
                text-variant="white"
                :header="item"
                class="text-center p-0"
              >
                <b-card-text v-for="i in limitItems(items,item) " v-bind:key="i.dt_txt">
                  {{i.time}}
                  <b-table-simple hover small responsive class="tabledata">
                    <b-thead small>
                      <b-tr>
                        <b-th small>Temprature</b-th>
                        <b-th>Humidity</b-th>
                        <b-th>Wind speed</b-th>
                      </b-tr>
                    </b-thead>
                    <b-tbody>
                      <b-tr>
                        <b-td :class="tabledata">{{i.main.temp}}</b-td>
                        <b-td>{{i.main.humidity}}</b-td>
                        <b-td>{{i.wind.speed}}</b-td>
                      </b-tr>
                    </b-tbody>
                  </b-table-simple>
                </b-card-text>
              </b-card>
            </b-card-group>
          </b-col>
        </b-row>
      </b-col>
      <b-col cols="1">
        <h2>History</h2>
        <div v-for="(city, n) in cities" v-bind:key="n">
          <p>
            <span>{{ city }}</span>
            <br />
          </p>
        </div>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
import WeatherForecastService from "@/api-services/weather-service";
export default {
  name: "Weather",
  props: {
    msg: String
  },
  data() {
    return {
      cities: [],
      items: [],
      dayItems: [],
      city: "",
      fields: [
        { key: "day", label: "Day" },
        { key: "time", label: "Time" },
        { key: "main.temp", label: "Temprature" },
        { key: "main.humidity", label: "Humidity" },
        { key: "wind.speed", label: "Wind speed" }
      ]
    };
  },
  mounted() {
    if (localStorage.getItem("cities")) {
      try {
        this.cities = JSON.parse(localStorage.getItem("cities"));
      } catch (e) {
        throw e;
      }
    }
  },
  methods: {
    getWeatherForecast: function() {
      WeatherForecastService.getForecast(this.SearchText).then(response => {
        this.items = response.data.list;
        var obj = {};
        var pday;
        response.data.list.forEach(e => {
          if (pday != e.day) {
            obj[e.day] = e.day;
          }
          pday = e.day;
        });
        this.dayItems = obj;
        this.city = response.data.city;

        // save to city to local storage as history
        this.cities.push(this.city);
        this.saveCities();
      });
      // .catch(error => {
      //   console.log(error.response.data);
      // });
    },
    limitItems(arr, item) {
      if (arr && arr.length) {
        var newArray = arr.filter(function(el) {
          return el.day === item;
        });
        return newArray;
      }
      return null;
    },
    saveCities() {
      const parsed = JSON.stringify(this.cities);
      localStorage.setItem("cities", parsed);
    }
  }
};
</script>
<style scoped>
.tabledata {
  font-size: xx-small;
}
</style>