package com.example.insurance_discounts

import MySharedPreferences
import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.view.MenuItem
import android.widget.Toast
import androidx.appcompat.app.ActionBarDrawerToggle
import androidx.appcompat.app.AppCompatActivity
import androidx.appcompat.widget.Toolbar
import androidx.core.view.GravityCompat
import androidx.drawerlayout.widget.DrawerLayout
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.insurance_discounts.adapters.CompaniesAdapter
import com.example.insurance_discounts.clients.CompaniesClient
import com.google.android.material.navigation.NavigationView
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.launch

class CompaniesActivity : AppCompatActivity(),
    NavigationView.OnNavigationItemSelectedListener{
    private lateinit var recyclerView: RecyclerView
    private lateinit var adapter: CompaniesAdapter
    private lateinit var drawerLayout: DrawerLayout
    private lateinit var sharedPreferences: MySharedPreferences

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_companies)

        sharedPreferences = MySharedPreferences(this)

        drawerLayout = findViewById<DrawerLayout>(R.id.drawer_layout)
        val toolbar = findViewById<Toolbar>(R.id.toolbar)
        setSupportActionBar(toolbar)
        val navigationView = findViewById<NavigationView>(R.id.nav_view)

        navigationView.setNavigationItemSelectedListener(this)
        val toggle = ActionBarDrawerToggle(
            this,
            drawerLayout,
            toolbar,
            R.string.open_nav,
            R.string.close_nav
        )

        drawerLayout.addDrawerListener(toggle)
        toggle.syncState()

        if(sharedPreferences.getPageId() == null || sharedPreferences.getPageId() == 0){
            navigationView.setCheckedItem(R.id.nav_mainPage)
        }
        else{
            val id = sharedPreferences.getPageId()
            navigationView.setCheckedItem(id!!);
        }

        recyclerView = findViewById(R.id.companiesRecyclerView)
        recyclerView.layoutManager = LinearLayoutManager(this)
        adapter = CompaniesAdapter()
        recyclerView.adapter = adapter

        // Make the HTTP request to retrieve companies using Retrofit
        getCompanies()
    }

    private fun getCompanies() {
        val service = CompaniesClient.companiesService
        GlobalScope.launch(Dispatchers.Main) {
            try {
                val companies = service.getCompanies()
                println(companies);
                adapter.setCompanies(companies)
            } catch (e: Exception) {
                Toast.makeText(
                    this@CompaniesActivity,
                    "Error fetching companies",
                    Toast.LENGTH_SHORT
                ).show()
                Log.e("CategoriesActivity", "Error: ${e.message}", e)
            }
        }
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
            val intent = Intent(this, CategoriesActivity::class.java)
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
}
