#include <iostream>
#include <vector>
#include <unordered_map>
#include <map>
#include <set>


int main() {
    std::unordered_map<std::string, int> m;
    std::string s;
    std::ios_base::sync_with_stdio(false);
    std::cin.tie(nullptr);
    while (std::cin >> s) {
        ++m[s];
    }
    std::map<int, std::set<std::string>> rev_count;
    for (auto p: m) {
        rev_count[-p.second].insert(p.first);
    }
    for (auto p: rev_count) {
        for (auto word: p.second) {
            std::cout << word << "\t" << -p.first << "\n";
        }
    }
}
