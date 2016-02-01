#include <iostream>
#include <vector>
#include <unordered_map>
#include <map>
#include <set>
#include <algorithm>

class F {
public:
    bool operator()(std::pair<int, std::vector<std::string>> const& lhs, std::pair<int, std::vector<std::string>> const& rhs) {
        return lhs.first < rhs.first;
    }
};


int main() {
    std::unordered_map<std::string, int> m;
    std::string s;
    std::ios_base::sync_with_stdio(false);
    std::cin.tie(nullptr);
    while (std::cin >> s) {
        ++m[s];
    }
    std::unordered_map<int, std::vector<std::string>> rev_count_map;
    for (auto p: m) {
        rev_count_map[-p.second].push_back(p.first);
    }
    std::vector<std::pair<int, std::vector<std::string>>> rev_count;
    for (auto p: rev_count_map) rev_count.push_back(std::pair<int, std::vector<std::string>>{p.first, p.second});
    std::sort(rev_count.begin(), rev_count.end(), F());
    for (auto& p: rev_count) {
        std::sort(p.second.begin(), p.second.end());
        for (auto word: p.second) {
            std::cout << word << "\t" << -p.first << "\n";
        }
    }
}
