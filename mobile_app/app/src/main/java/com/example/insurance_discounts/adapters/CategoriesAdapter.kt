package com.example.insurance_discounts.adapters

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.example.insurance_discounts.data.Category
import com.example.insurance_discounts.databinding.ItemCategoryBinding

class CategoriesAdapter() :
    RecyclerView.Adapter<CategoriesAdapter.ViewHolder>() {
    private val categories: MutableList<Category> = mutableListOf()

    fun setCategories(categories: List<Category>){
        this.categories.clear()
        this.categories.addAll(categories)
        notifyDataSetChanged()
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val inflater = LayoutInflater.from(parent.context)
        val binding = ItemCategoryBinding.inflate(inflater, parent, false)
        return ViewHolder(binding)
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        val category = categories[position]
        holder.bind(category)
    }

    override fun getItemCount(): Int {
        return categories.size
    }

    inner class ViewHolder(private val binding: ItemCategoryBinding) :
        RecyclerView.ViewHolder(binding.root) {

        fun bind(category: Category) {
            binding.categoryNameTextView.text = category.categoryName
        }
    }
}
