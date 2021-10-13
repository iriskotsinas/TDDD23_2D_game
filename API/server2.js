const express =  require('express');
const tfnode = require('@tensorflow/tfjs-node');
var fs = require("fs");
const p5 = require('node-p5');
var text = fs.readFileSync("./class_names.txt", "utf-8");
//var categories = text.split("\n")
const categories = ['flashlight', 'belt', 'mushroom', 'pond', 'strawberry', 'pineapple', 'sun', 'cow', 'ear', 'bush', 'pliers', 'watermelon', 'apple', 'baseball', 'feather', 'shoe', 'leaf', 'lollipop', 'crown', 'ocean', 'horse', 'mountain', 'mosquito', 'mug', 'hospital', 'saw', 'castle', 'angel', 'underwear', 'traffic_light', 'cruise_ship', 'marker', 'blueberry', 'flamingo', 'face', 'hockey_stick', 'bucket', 'campfire', 'asparagus', 'skateboard', 'door', 'suitcase', 'skull', 'cloud', 'paint_can', 'hockey_puck', 'steak', 'house_plant', 'sleeping_bag', 'bench', 'snowman', 'arm', 'crayon', 'fan', 'shovel', 'leg', 'washing_machine', 'harp', 'toothbrush', 'tree', 'bear', 'rake', 'megaphone', 'knee', 'guitar', 'calculator', 'hurricane', 'grapes', 'paintbrush', 'couch', 'nose', 'square', 'wristwatch', 'penguin', 'bridge', 'octagon', 'submarine', 'screwdriver', 'rollerskates', 'ladder', 'wine_bottle', 'cake', 'bracelet', 'broom', 'yoga', 'finger', 'fish', 'line', 'truck', 'snake', 'bus', 'stitches', 'snorkel', 'shorts', 'bowtie', 'pickup_truck', 'tooth', 'snail', 'foot', 'crab', 'school_bus', 'train', 'dresser', 'sock', 'tractor', 'map', 'hedgehog', 'coffee_cup', 'computer', 'matches', 'beard', 'frog', 'crocodile', 'bathtub', 'rain', 'moon', 'bee', 'knife', 'boomerang', 'lighthouse', 'chandelier', 'jail', 'pool', 'stethoscope', 'frying_pan', 'cell_phone', 'binoculars', 'purse', 'lantern', 'birthday_cake', 'clarinet', 'palm_tree', 'aircraft_carrier', 'vase', 'eraser', 'shark', 'skyscraper', 'bicycle', 'sink', 'teapot', 'circle', 'tornado', 'bird', 'stereo', 'mouth', 'key', 'hot_dog', 'spoon', 'laptop', 'cup', 'bottlecap', 'The_Great_Wall_of_China', 'The_Mona_Lisa', 'smiley_face', 'waterslide', 'eyeglasses', 'ceiling_fan', 'lobster', 'moustache', 'carrot', 'garden', 'police_car', 'postcard', 'necklace', 'helmet', 'blackberry', 'beach', 'golf_club', 'car', 'panda', 'alarm_clock', 't-shirt', 'dog', 'bread', 'wine_glass', 'lighter', 'flower', 'bandage', 'drill', 'butterfly', 'swan', 'owl', 'raccoon', 'squiggle', 'calendar', 'giraffe', 'elephant', 'trumpet', 'rabbit', 'trombone', 'sheep', 'onion', 'church', 'flip_flops', 'spreadsheet', 'pear', 'clock', 'roller_coaster', 'parachute', 'kangaroo', 'duck', 'remote_control', 'compass', 'monkey', 'rainbow', 'tennis_racquet', 'lion', 'pencil', 'string_bean', 'oven', 'star', 'cat', 'pizza', 'soccer_ball', 'syringe', 'flying_saucer', 'eye', 'cookie', 'floor_lamp', 'mouse', 'toilet', 'toaster', 'The_Eiffel_Tower', 'airplane', 'stove', 'cello', 'stop_sign', 'tent', 'diving_board', 'light_bulb', 'hammer', 'scorpion', 'headphones', 'basket', 'spider', 'paper_clip', 'sweater', 'ice_cream', 'envelope', 'sea_turtle', 'donut', 'hat', 'hourglass', 'broccoli', 'jacket', 'backpack', 'book', 'lightning', 'drums', 'snowflake', 'radio', 'banana', 'camel', 'canoe', 'toothpaste', 'chair', 'picture_frame', 'parrot', 'sandwich', 'lipstick', 'pants', 'violin', 'brain', 'power_outlet', 'triangle', 'hamburger', 'dragon', 'bulldozer', 'cannon', 'dolphin', 'zebra', 'animal_migration', 'camouflage', 'scissors', 'basketball', 'elbow', 'umbrella', 'windmill', 'table', 'rifle', 'hexagon', 'potato', 'anvil', 'sword', 'peanut', 'axe', 'television', 'rhinoceros', 'baseball_bat', 'speedboat', 'sailboat', 'zigzag', 'garden_hose', 'river', 'house', 'pillow', 'ant', 'tiger', 'stairs', 'cooler', 'see_saw', 'piano', 'fireplace', 'popsicle', 'dumbbell', 'mailbox', 'barn', 'hot_tub', 'teddy-bear', 'fork', 'dishwasher', 'peas', 'hot_air_balloon', 'keyboard', 'microwave', 'wheel', 'fire_hydrant', 'van', 'camera', 'whale', 'candle', 'octopus', 'pig', 'swing_set', 'helicopter', 'saxophone', 'passport', 'bat', 'ambulance', 'diamond', 'goatee', 'fence', 'grass', 'mermaid', 'motorbike', 'microphone', 'toe', 'cactus', 'nail', 'telephone', 'hand', 'squirrel', 'streetlight', 'bed', 'firetruck'];

vehicles = ["aircraft_carrier", "airplane", "ambulance", "bathtub", "wheel", "van", "truck", "train", "police_car", "tractor", "toilet", "submarine", "speedboat", "skateboard",  "bicycle", "bus", "cruise_ship", "flying_saucer", "helicopter", "hot_air_balloon", "motorbike", "pickup_truck", "rollerskates", "sailboat", "school_bus", "bulldozer", "canoe", "car", "firetruck"]
weapons = ["anvil", "arm", "axe", "baseball", "baseball_bat", "basket", "basketball", "violin", "vase", "umbrella", "trumpet", "trombone", "triangle", "toothbrush", "tennis_racquet", "syringe", "sword", "suitcase", "stop_sign", "spoon", "soccer_ball", "shovel", "boomerang", "broom", "cactus", "cannon", "drill", "fire_hydrant", "fork", "frying_pan", "golf_club", "guitar", "hammer", "hockey_puck", "hockey_stick", "lighter", "pencil", "rifle", "saw", "scissors", "screwdriver", "clarinet", "crayon", "garden_hose", "harp", "knife", "rake"]
animals = ["ant", "bat", "bee", "butterfly", "dog", "horse", "mermaid", "bear", "bird", "camel", "cow", "crab", "crocodile", "dolphin", "dragon", "elephant", "fish", "flamingo", "frog", "giraffe", "hedgehog", "lobster", "monkey", "octopus", "owl",
"panda", "parrot", "pig", "rabbit", "raccoon", "rhinoceros", "scorpion", "sea_turtle", "shark", "snail", "spider", "squirrel", "teddy-bear", "zebra", "whale", "tiger", "swan", "snake", "cat", "duck", "lion", "mosquito", "mouse", "sheep", "kangaroo"]
foods = ["apple", "asparagus", "banana", "birthday_cake", "blackberry", "blueberry", "carrot", "coffee_cup", "mushroom", "peas", "pineapple", "pizza", "sandwich", "strawberry", "string_bean", "teapot", "toaster",
"watermelon", "wine_bottle", "wine_glass", "toothpaste", "steak",  "bread", "broccoli", "cake", "cookie", "donut", "grapes", "hamburger", "ice_cream", "lollipop", "peanut", "pear", "popsicle", "potato", "onion"]
armors = ["bandage", "beard", "belt", "binoculars", "bowtie", "bracelet", "brain", "wristwatch", "underwear", "t-shirt", "goatee", "tooth", "toe", "sweater", "stitches", "stethoscope", "sock", "snorkel", "smiley_face", "sleeping_bag", "skull",  "crown", "eyeglasses",
"face", "finger", "foot", "hand", "hat", "headphones", "jacket", "lipstick", "moustache", "mouth", "nail", "necklace", "pants", "purse", "shoe", "shorts",  "camouflage", "cooler", "ear", "elbow", "eye", "flip_flops", "knee", "leg", "nose", "clock", "diamond", "helmet"]
others = ["alarm_clock", "animal_migration", "backpack", "zigzag", "yoga", "windmill", "waterslide", "washing_machine", "tree", "angel", "The_Mona_Lisa", "traffic_light", "tornado", "The_Eiffel_Tower", "The_Great_Wall_of_China", "tent", "television", "telephone", "table", "swing_set", "sun", "streetlight", "stove", "stereo", "star", "stairs", "spreadsheet", "square", "squiggle", "snowflake", "snowman", "skyscraper", "sink", "barn", "beach", "bed", "bench", "book", "bottlecap", "bridge", "bucket", "bush", "calculator", "calendar", "camera", "castle", "ceiling_fan", "cello", "couch", "cup", "dishwasher", "diving_board", "fence", "fireplace", "floor_lamp", "hexagon", "paper_clip", "picture_frame", "radio", "rain", "remote_control", "saxophone", "see_saw",
"hospital", "jail", "keyboard", "light_bulb", "lighthouse", "lightning", "mailbox", "moon", "mountain", "ocean", "octagon", "oven", "palm_tree", "passport", "penguin", "piano", "pillow", "pond", "pool", "postcard", "power_outlet", "rainbow", "river", "roller_coaster", "campfire", "candle", "cell_phone", "chair", "chandelier", "church", "circle", "cloud", "compass", "computer", "door", "dresser", "drums", "dumbbell", "envelope", "eraser", "fan", "feather", "flashlight", "flower", "garden", "grass", "hot_dog", "hot_tub", "hourglass", "house", "house_plant", "hurricane", "key", "ladder", "lantern", "laptop", "leaf", "line", "map", "marker", "matches", "megaphone", "microphone", "microwave", "mug", "paintbrush", "paint_can", "parachute", "pliers"]

all_cat = [vehicles, weapons, animals, foods, armors, others]
all_cat_names = ['vehicles', 'weapons', 'animals', 'foods', 'armors', 'others']

// Load the model.

tfnode.loadLayersModel('file://model.json').then(model => {
     
const app = express();

const PORT = 8080;

app.use(express.json());

app.post('/image', (req, res) => {

    const {b64_image} = req.body;

    const buffer = Buffer.from(b64_image, 'base64');
    let img = tfnode.node.decodeImage(buffer,3, 'int32');
    img = tfnode.image.resizeBilinear(img,[28,28])

    const n = tfnode.scalar(255.0);
    img = tfnode.cast(img, 'float32');
    
    const inputs = Array.from(img.dataSync());
    const final_img = [];

    let oneRow = [];
    for (let i = 0; i < 784; i++) {
        let bright = inputs[i * 3]; // rbg
        let onePix = [parseFloat((255 - bright) / 255)]; //Black <-> White
        oneRow.push(onePix);
        if (oneRow.length === 28) {
            final_img.push(oneRow);
            oneRow = [];
        }
    }

    let guess = model.predict(tfnode.tensor([final_img]));
    const predictions = Array.from(guess.dataSync());

     
    // Get top K res with index and probability
    const rawProbWIndex = predictions.map((probability, index) => {
        return {
        index,
        probability
        }
    });

    top5 = rawProbWIndex.sort((a, b) => b.probability - a.probability);
    top5 = rawProbWIndex.slice(0, 5);

    const list_of_pred = [];

    top5.map((item) => {

        let groupName = "";

        for (let i = 0; i < all_cat.length; i++) {

            const found = all_cat[i].includes(categories[item.index]);
            if(found){
                groupName = all_cat_names[i]; 
                break;
            }
        }
        list_of_pred.push({prediction: categories[item.index], confidence: item.probability, group: groupName});
    });

    if(!b64_image){
        res.status(418).send({message: "Error, we need image"})
    }

    best_pred = list_of_pred[0];

    res.send({prediction: `${best_pred.prediction}`, confidence: `${best_pred.confidence}`, group: `${best_pred.group}`, other_pred: list_of_pred, })
});

app.get('/image', (req,res) => {

    res.send({hello: 'hello'})
});


app.listen(PORT, 
    () => {
        console.log(`its alive on http://localhost:${PORT}`);
        //test = ml5.imageClassifier('DoodleNet');
}
)
});
   


