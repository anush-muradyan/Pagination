namespace Data.Model {
    public class DataEntry {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Thumbnail Thumbnail { get; set; }

        public long CreatedAt { get; set; }

        public override string ToString() {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Price)}: " +
                   $"{Price}, {nameof(Thumbnail)}: {Thumbnail}, {nameof(CreatedAt)}: {CreatedAt}";
        }
    }
}