package com.example.insurance_discounts

import MySharedPreferences
import android.content.Intent
import android.os.Bundle
import android.view.MenuItem
import android.widget.Toast
import androidx.appcompat.app.ActionBarDrawerToggle
import androidx.appcompat.app.AppCompatActivity
import androidx.appcompat.widget.Toolbar
import androidx.core.view.GravityCompat
import androidx.drawerlayout.widget.DrawerLayout
import androidx.recyclerview.widget.LinearLayoutManager
import com.auth0.android.jwt.JWT
import com.example.insurance_discounts.adapters.InsurancesAdapter
import com.example.insurance_discounts.clients.InsurancesClient
import com.example.insurance_discounts.data.Insurance
import com.example.insurance_discounts.data.InsurancesResponse
import com.example.insurance_discounts.databinding.ActivityInsurancesBinding
import com.google.android.material.navigation.NavigationView
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response


class MainPageActivity: AppCompatActivity(),
    NavigationView.OnNavigationItemSelectedListener {
    private lateinit var binding: ActivityInsurancesBinding
    private lateinit var insurancesAdapter: InsurancesAdapter
    private lateinit var drawerLayout: DrawerLayout
    private lateinit var navigationView: NavigationView
    private lateinit var sharedPreferences: MySharedPreferences


    override fun onCreate(savedInstanceState: Bundle?){
        super.onCreate(savedInstanceState)
        binding = ActivityInsurancesBinding.inflate(layoutInflater)
        setContentView(binding.root);

        sharedPreferences = MySharedPreferences(this)

        drawerLayout = findViewById<DrawerLayout>(R.id.drawer_layout)
        val toolbar = findViewById<Toolbar>(R.id.toolbar)
        setSupportActionBar(toolbar)
        navigationView = findViewById<NavigationView>(R.id.nav_view)

        navigationView.setNavigationItemSelectedListener(this)
        val toggle = ActionBarDrawerToggle(this, drawerLayout, toolbar, R.string.open_nav, R.string.close_nav)

        drawerLayout.addDrawerListener(toggle)
        toggle.syncState()


        if(sharedPreferences.getPageId() == null || sharedPreferences.getPageId() == 0){
            navigationView.setCheckedItem(R.id.nav_mainPage)
        }
        else{
            val id = sharedPreferences.getPageId()
            navigationView.setCheckedItem(id!!);
        }

        setupRecyclerView()
        fetchInsurances()
    }

    override fun onNavigationItemSelected(item: MenuItem): Boolean {


        if(item.itemId == R.id.nav_personalPage){
            val intent = Intent(this, UserActivity::class.java)
            startActivity(intent)
        }

        if(item.itemId == R.id.nav_mainPage){
            val intent = Intent(this, MainPageActivity::class.java)
            startActivity(intent)
        }

        if(item.itemId == R.id.nav_companies){
            val intent = Intent(this, CompaniesActivity::class.java)
            startActivity(intent)
        }

        if(item.itemId == R.id.nav_agents){
            val intent = Intent(this, AgentsActivity::class.java)
            startActivity(intent)
        }

        if(item.itemId == R.id.nav_categories){
            val intent = Intent(this@MainPageActivity, CategoriesActivity::class.java)
            startActivity(intent)
        }

        if(item.itemId == R.id.nav_about){
            val intent = Intent(this, AboutUsActivity::class.java)
            startActivity(intent);
        }

        if(item.itemId == R.id.nav_logout){
            sharedPreferences.clearJwt()
            sharedPreferences.clearPageId()
            val intent = Intent(this, MainActivity::class.java)
            startActivity(intent)
        }

        sharedPreferences.savePageId(item.itemId)
        drawerLayout.closeDrawer(GravityCompat.START)
        return true
    }

    override fun onBackPressed() {
        if (drawerLayout.isDrawerOpen(GravityCompat.START)) {
            drawerLayout.closeDrawer(GravityCompat.START)
        } else {
            onBackPressedDispatcher.onBackPressed()
        }
    }

    private fun setupRecyclerView() {
        val token = sharedPreferences.getJwt()
        val userId = extractIdFromToken(token!!)

        insurancesAdapter = InsurancesAdapter(userId!!)
        binding.recyclerView.apply {
            adapter = insurancesAdapter
            layoutManager = LinearLayoutManager(this@MainPageActivity)
        }
    }

    private fun extractIdFromToken(token: String): String? {
        try {
            // Decode the JWT token
            val jwt = JWT(token)
            return jwt.getClaim("UserId").asString()
        } catch (e: Exception) {
            e.printStackTrace()
        }
        return null
    }

    private fun fetchInsurances() {
       val client = InsurancesClient()

       client.getAllInsurances().enqueue(object: Callback<List<Insurance>>{
           override fun onResponse(call: Call<List<Insurance>>, response: Response<List<Insurance>>) {
               if (response.isSuccessful) {
                   val insurances = response.body()
                   println(insurances)
                   insurances?.let {
                       insurancesAdapter.setInsurances(insurances)
                   }
               } else {
                   // Handle API error
                   Toast.makeText(
                       this@MainPageActivity,
                       "Response is not succeeded",
                       Toast.LENGTH_SHORT
                   ).show()
               }
           }

            override fun onFailure(call: Call<List<Insurance>>, t: Throwable) {
                Toast.makeText(
                    this@MainPageActivity,
                    "Failed to fetch insurances: ${t.message}",
                    Toast.LENGTH_SHORT
                ).show()
            }
        })
    }
}